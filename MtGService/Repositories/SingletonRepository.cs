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

        private static readonly string _SlackEndpoint = "https://hooks.slack.com/services/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";

        
        
        public MelekDataStore MelekDataStore { get { return _MelekDataStore; } }

        public string SlackEndpoint { get { return _SlackEndpoint; } }
    }
}