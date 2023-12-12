using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using WrongHole.Components;
using WrongHole.StateManagement;
using WrongHole.Utils;

namespace WrongHole.Screens
{
    public class ButtonMenuScreen : GameScreen
    {
        protected List<Button> _buttons;

        protected ContentManager _content;

        protected GraphicsDevice _graphics;

        protected MouseState _mouseStateLast;

        protected Monochrome _monochrome;

        public ButtonMenuScreen()
        {
        }

        public override void Activate()
        {
            if (_content == null)
                _content = new ContentManager(ScreenManager.Game.Services, "Content");
            _graphics = ScreenManager.GraphicsDevice;

            _buttons = new List<Button>();

            _monochrome = Constants.MONOCHROMES[new Random().Next(Constants.MONOCHROMES.Length)];

            _mouseStateLast = Mouse.GetState();
        }

        public override void Update(GameTime gameTime, bool otherScreenHasFocus, bool coveredByOtherScreen)
        {
            base.Update(gameTime, otherScreenHasFocus, coveredByOtherScreen);

            foreach (var button in _buttons) button.Update(_mouseStateLast, Mouse.GetState());

            _mouseStateLast = Mouse.GetState();
        }

        public override void Draw(GameTime gameTime)
        {
            var spriteBatch = ScreenManager.SpriteBatch;

            spriteBatch.Begin();

            base.Draw(gameTime);

            var texture = new Texture2D(ScreenManager.GraphicsDevice, 1, 1);
            texture.SetData(new Color[] { _monochrome.Background });

            spriteBatch.Draw(
                texture,
                new Rectangle(Point.Zero, Tools.ToPoint(Constants.GAME_FULL)),
                Color.White);

            foreach (var button in _buttons) button.Draw(spriteBatch);

            spriteBatch.End();
        }

        protected virtual TextButton GetTextButton(Point center, string text)
        {
            return new TextButton(
                _content,
                center,
                text,
                "square",
                ScreenManager.Font,
                _monochrome.Button,
                _monochrome.ButtonSelected,
                _monochrome.ButtonText);
        }
    }
}
