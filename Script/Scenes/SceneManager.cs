using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;

namespace HashGame
{
    public class Manager_Scene : Game
    {

        private GraphicsDeviceManager _graphics;

        public GraphicsDeviceManager Graphics
        {
            get => _graphics;
        }

        private SpriteBatch _spriteBatch;

        public Scene currentScene = new();

        public List<Text> texts = new();
        public List<Button> buttons = new();

        public int layer;

        Song music;

        public string issong;
        private bool play;
        public bool Play
        {
            get => play;
            set
            {
                if (value)
                {
                    issong = "ON";
                    MediaPlayer.Play(music);
                }
                else
                {
                    issong = "OFF";
                    MediaPlayer.Stop();
                }
                play = value;
            }
        }


        public Manager_Scene()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
            Window.AllowUserResizing = true;
            Window.Title = "HashGame";
        }

        protected override void Initialize()
        {
            // TODO: Add your initialization logic here
            Window.ClientSizeChanged += new EventHandler<EventArgs>(Window_ClientSizeChanged);
            SwitchScene(new Title_Scene());
            layer = 1;

            base.Initialize();
        }
        private void Window_ClientSizeChanged(object sender, EventArgs e)
        {
            _graphics.PreferredBackBufferWidth = Window.ClientBounds.Width;
            _graphics.PreferredBackBufferHeight = Window.ClientBounds.Height;
            _graphics.ApplyChanges();
        }


        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);
            // TODO: use this.Content to load your game content here

            music = Content.Load<Song>("Music");

            Play = true;
        }

        protected override void Update(GameTime gameTime)
        {
            currentScene.Update(gameTime);
            var lButtons = new List<Button>();
            lButtons.AddRange(buttons);

            lButtons.ForEach(b => b.Update(gameTime));


            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();


            // TODO: Add your update logic here
            base.Update(gameTime);
        }


        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.Black);

            texts.ForEach(t => t.Draw(gameTime));
            buttons.ForEach(b => b.Draw(gameTime));

            currentScene.Draw(gameTime);
            base.Draw(gameTime);
        }
        protected override void Dispose(bool disposing)
        {
            Content.Unload();
            music.Dispose();
            base.Dispose(disposing);
        }


        public void SwitchScene(Scene scene)
        {
            buttons.Clear();
            texts.Clear();
            scene.scene = this;
            scene.Initialize();
            currentScene = scene;
        }
    }
}