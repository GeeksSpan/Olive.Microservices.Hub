﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Xml.Linq;
using Olive;
using Olive.Entities;
using Olive.Entities.Data;

namespace Olive.Microservices.Hub
{
    partial class Board
    {
        public static IEnumerable<Board> All { get; internal set; }

        public Board(XElement data)
        {
            Name = data.GetCleanName();

            widgets = data.Elements().SelectMany(group => group.Elements().Select(x =>
              new Widget
              {
                  Board = this,
                  Group = group.Name.LocalName,
                  Title = x.GetCleanName(),
                  Colour = x.GetValue<string>("@colour"),
                  Feature = Feature.FindByRef(x.GetValue<string>("@feature")) ?? throw new Exception("Feature not specified!"),
                  Settings = Feature.FindByRef(x.GetValue<string>("@settings"))
              })).ToList();
        }

        public Widget[] GetWidgets(ClaimsPrincipal user)
        {
            return Widgets.Where(x => user.CanSee(x.Feature) && x.Feature.ShowOnRight == false).ToArray();
        }

        public Widget GetRightWidget(ClaimsPrincipal user)
        {
            return Widgets.FirstOrDefault(x => user.CanSee(x.Feature) && x.Feature.ShowOnRight);
        }

        public static Board Parse(string name) => All.FirstOrDefault(x => x.Name == name);

        public class DataProvider : LimitedDataProvider
        {
            public static void Register()
            {
                var config = Context.Current.GetService<IDatabaseProviderConfig>();
                config.RegisterDataProvider(typeof(Board), new DataProvider());
            }

            public override Task<IEntity> Get(object objectID)
            {
                var id = objectID.ToString().To<Guid>();
                return Task.FromResult((IEntity)All.First(x => x.ID == id));
            }
        }
    }
}