using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Hosting;
using Melek.DataStore;
using Melek.Utilities;

namespace MtGService.Repositories
{
    public class SingletonRepository
    {
        private static readonly MelekDataStore _MelekDataStore = new MelekDataStore(Path.Combine(HostingEnvironment.MapPath("~/Content/"), "MelekData"), false, new LoggingNinja(HostingEnvironment.MapPath("~/Content/MelekData/errors.log")));

        public MelekDataStore MelekDataStore { get { return _MelekDataStore; } }
    }
}