﻿using MockSQL.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MockSQL.Models
{
    public class Procedure
    {
        private readonly Lazy<IEnumerable<Parameter>> parameters;

        public Procedure()
        {
            parameters = new Lazy<IEnumerable<Parameter>>(() => RepositoryFactory.GetRepository().GetParameters(this));
        }

        public IList<Parameter> Parameters
        {
            get=> new List<Parameter>(parameters.Value);
        }

        public string Name { get; set; }
        public string Definition { get; set; }
        public Database Database { get; set; }

        public override string ToString() => Name;
       
    }
}