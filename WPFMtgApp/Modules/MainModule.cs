using Ninject.Modules;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WPFMtgApp.BusinessLogic;

namespace WPFMtgApp.Modules
{
    public class MainModule : NinjectModule
    {
        public override void Load()
        {
            Bind<ICardFetcherService>().To<CardFetcherService>().InSingletonScope();
        }
    }
}
