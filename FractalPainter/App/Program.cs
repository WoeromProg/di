using System;
using System.Windows.Forms;
using FractalPainting.Infrastructure.Common;
using FractalPainting.Infrastructure.UiActions;
using Ninject;
using Ninject.Extensions.Conventions;
using Ninject.Extensions.Factory;

namespace FractalPainting.App
{
    internal static class Program
    {
        /// <summary>
        ///     The main entry point for the application.
        /// </summary>
        [STAThread]
        private static void Main()
        {
            try
            {
                var container = new StandardKernel();
                
                container
                    .Bind(k => k
                        .FromThisAssembly()
                        .SelectAllClasses()
                        .InheritedFrom<IUiAction>()
                        .BindAllInterfaces());

                container
                    .Bind<IImageHolder, PictureBoxImageHolder>()
                    .To<PictureBoxImageHolder>()
                    .InSingletonScope();

                container
                    .Bind<Palette>()
                    .ToSelf()
                    .InSingletonScope();

                container
                    .Bind<IDragonPainterFactory>()
                    .ToFactory();

                container
                    .Bind<IObjectSerializer, XmlObjectSerializer>()
                    .To<XmlObjectSerializer>();

                container
                    .Bind<IBlobStorage, FileBlobStorage>()
                    .To<FileBlobStorage>();

                container
                    .Bind<IImageDirectoryProvider>()
                    .ToMethod(k => k.Kernel.Get<SettingsManager>().Load());

                container
                    .Bind<ImageSettings>()
                    .ToMethod(k => k.Kernel.Get<SettingsManager>().Load().ImageSettings);

                // container
                //     .Bind<ImageSettings>()
                //     .ToMethod(k => k.Kernel.Get<AppSettings>().ImageSettings);

                Application.EnableVisualStyles();
                Application.SetCompatibleTextRenderingDefault(false);
                Application.Run(container.Get<MainForm>());
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
            }
        }
    }
}