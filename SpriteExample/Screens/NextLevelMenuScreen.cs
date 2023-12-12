using System;
using Microsoft.Xna.Framework.Input;
using WrongHole.StateManagement;
using WrongHole.Utils;

namespace WrongHole.Screens
{
    public class NextLevelMenuScreen : ButtonMenuScreen
    {
        protected TileMapLevelScreen _currLvl; // This screen should be already removed from the screen manager.

        public NextLevelMenuScreen(TileMapLevelScreen currentLevel) : base()
        {
            _currLvl = currentLevel;
        }

        public override void Activate()
        {
            base.Activate();

            var nextLvl = WrongHoleGame.score[this._currLvl.CurrentLevel] && this._currLvl.CurrentLevel + 1 < WrongHoleGame.score.Length;

            var buttonTexts =
                nextLvl
                    ? new string[] { "Replay", "Next Level", "Menu" }
                    : new string[] { "Replay", "Menu" };
            var buttonCenters = Tools.AlignButtonsHorizontally(buttonTexts, ScreenManager.Font);

            var buttonReplay = GetTextButton(
                buttonCenters[0],
                buttonTexts[0]);
            buttonReplay.ButtonEventHandler += ReplayLevel;
            _buttons.Add(buttonReplay);

            if (nextLvl)
            {
                var buttonNextLevel = GetTextButton(
                    buttonCenters[1],
                    buttonTexts[1]);
                buttonNextLevel.ButtonEventHandler += NextLevel;
                _buttons.Add(buttonNextLevel);
            }

            var buttonMenu = GetTextButton(
                buttonCenters[nextLvl ? 2 : 1],
                buttonTexts[nextLvl ? 2 : 1]);
            buttonMenu.ButtonEventHandler += ExitToMenu;
            _buttons.Add(buttonMenu);

            _mouseStateLast = Mouse.GetState();
        }

        private void ReplayLevel(object sender, EventArgs e)
        {
            ScreenManager.RemoveScreen(this);

            ScreenManager.AddScreen(new TileMapLevelScreen(this._currLvl.CurrentLevel), null);
        }

        private void NextLevel(object sender, EventArgs e)
        {
            ScreenManager.RemoveScreen(this);

            ScreenManager.AddScreen(new TileMapLevelScreen(++this._currLvl.CurrentLevel), null);
        }

        private void ExitToMenu(object sender, EventArgs e)
        {
            ScreenManager.RemoveScreen(this);

            ScreenManager.AddScreen(new IntroScreen(), null);
        }
    }
}
