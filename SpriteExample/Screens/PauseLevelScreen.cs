using System;
using Microsoft.Xna.Framework.Input;
using WrongHole.StateManagement;
using WrongHole.Utils;

namespace WrongHole.Screens
{
    public class PauseLevelScreen : ButtonMenuScreen
    {
        protected TileMapLevelScreen _currLvl;

        public PauseLevelScreen(TileMapLevelScreen currentLevel) : base()
        {
            _currLvl = currentLevel;
        }

        public override void Activate()
        {
            base.Activate();

            var paused = _currLvl._levelState == TileMapLevelScreen.LevelState.Pause;

            var buttonTexts =
                paused
                    ? new string[] { "Replay", "Resume", "Menu" }
                    : new string[] { "Replay", "Menu" };
            var buttonCenters = Tools.AlignButtonsHorizontally(buttonTexts, ScreenManager.Font);

            var buttonReplay = GetTextButton(
                buttonCenters[0],
                buttonTexts[0]);
            buttonReplay.ButtonEventHandler += ReplayLevel;
            _buttons.Add(buttonReplay);

            if (paused)
            {
                var buttonResumeLevel = GetTextButton(
                    buttonCenters[1],
                    buttonTexts[1]);
                buttonResumeLevel.ButtonEventHandler += ResumeLevel;
                _buttons.Add(buttonResumeLevel);
            }

            var buttonMenu = GetTextButton(
                buttonCenters[paused ? 2 : 1],
                buttonTexts[paused ? 2 : 1]);
            buttonMenu.ButtonEventHandler += ExitToMenu;
            _buttons.Add(buttonMenu);

            _mouseStateLast = Mouse.GetState();
        }

        private void ReplayLevel(object sender, EventArgs e)
        {
            ScreenManager.RemoveScreen(this);
            _currLvl.Activate();
        }

        private void ResumeLevel(object sender, EventArgs e)
        {
            ScreenManager.RemoveScreen(this);
            _currLvl.Resume();
        }

        private void ExitToMenu(object sender, EventArgs e)
        {
            ScreenManager.RemoveScreen(this);
            ScreenManager.RemoveScreen(_currLvl);

            ScreenManager.AddScreen(new IntroScreen(), null);
        }
    }
}
