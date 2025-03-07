﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Olive.Microservices.Hub.Utilities.UrlExtensions;
using Newtonsoft.Json;
using Olive;
using Olive.Entities;
using Olive.Entities.Data;

namespace Olive.Microservices.Hub
{
    public partial class Service
    {
        public static IEnumerable<Service> All { get; internal set; }
        public string FeaturesJsonPath() => $"/features/services/{Name}.json";
        public static string ToJson()
        {
            var items = All
                .Select(x => new { x.BaseUrl, x.Name })
                .ToList();

            return JsonConvert.SerializeObject(items);
        }

        protected override async Task OnValidating(EventArgs e)
        {
            await base.OnValidating(e);

            if (!BaseUrl.ToLower().StartsWith("http")) BaseUrl = BaseUrl.WithPrefix("http://");
        }

        public override async Task Validate()
        {
            await base.Validate();

            if (Name.UrlEncode() != Name)
                throw new ValidationException("Name is not in the correct format: " + Name.UrlEncode());
        }

        string GetHubImplementationUrlPrefix() => Name.ToLower().WithWrappers("[", "]");

        public string GetHubImplementationUrl(string relativeUrl)
            => GetHubImplementationUrlPrefix().AppendUrlPath(relativeUrl);

        public string GetAbsoluteImplementationUrl(string relativeUrl) => BaseUrl.AppendUrlPath(relativeUrl);

        public static Service FindByName(string name) => All.FirstOrDefault(s => s.Name == name);

        public class DataProvider : LimitedDataProvider
        {
            public static void Register()
            {
                var config = Context.Current.GetService<IDatabaseProviderConfig>();
                config.RegisterDataProvider(typeof(Service), new DataProvider());
            }

            public override Task<IEntity> Get(object objectID)
            {
                var id = objectID.ToString().To<Guid>();
                return Task.FromResult((IEntity)All.First(x => x.ID == id));
            }
        }
        public async Task GetAndSaveFeaturesJson()
        {
            var runtimeFeatures = new Mvc.Microservices.Feature[0];
            try
            {
                var url = (BaseUrl + "/olive/features").AsUri();
                runtimeFeatures = JsonConvert.DeserializeObject<Mvc.Microservices.Feature[]>(await url.Download(timeOutSeconds: 10));
            }
            catch (Exception ex)
            {
                Log.For(typeof(Features)).Warning(ex.ToString());
            }
            await Features.Repository.Write(FeaturesJsonPath(), JsonConvert.SerializeObject(runtimeFeatures));
        }
    }
}