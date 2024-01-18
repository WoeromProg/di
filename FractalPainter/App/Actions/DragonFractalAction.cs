using System;
using FractalPainting.App.Fractals;
using FractalPainting.Infrastructure.Common;
using FractalPainting.Infrastructure.Injection;
using FractalPainting.Infrastructure.UiActions;
using Ninject;

namespace FractalPainting.App.Actions
{
    public class DragonFractalAction : IUiAction
    {
        private readonly IDragonPainterFactory painterFactory;
        private readonly Func<Random, DragonSettingsGenerator> generatorFunc;

        public DragonFractalAction(
            IDragonPainterFactory painterFactory,
            Func<Random, DragonSettingsGenerator> generatorFunc)
        {
            this.painterFactory = painterFactory;
            this.generatorFunc = generatorFunc;
        }

        public Category Category => Category.Fractals;
        public string Name => "Дракон";
        public string Description => "Дракон Хартера-Хейтуэя";

        public void Perform()
        {
            var dragonSettings = generatorFunc(new Random()).Generate();
            // редактируем настройки:
            SettingsForm.For(dragonSettings).ShowDialog();
            // создаём painter с такими настройками
            painterFactory.CreateDragonPainter(dragonSettings).Paint();
        }

        private static DragonSettings CreateRandomSettings()
        {
            return new DragonSettingsGenerator(new Random()).Generate();
        }
    }
}