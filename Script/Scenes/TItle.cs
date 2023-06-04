using System;
using System.Security.Cryptography.X509Certificates;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;
using MonoGame.Extended;

namespace HashGame
{
    public class Title_Scene : Scene
    {
        public override void Initialize()
        {
            EngineText(new()
            {
                new Text("Simple", 80, Color.White, new(() => { return scene.Graphics.PreferredBackBufferWidth / 2; }), new(() => { return (scene.Graphics.PreferredBackBufferHeight / 2) - 150; }), Color.White, 0,true),
                new Text("HashGame", 80, Color.White, new(() => { return scene.Graphics.PreferredBackBufferWidth / 2; }), new(() => { return (scene.Graphics.PreferredBackBufferHeight / 2) - 75; }), Color.White, 0, true),
                new Text("Developed by: Otaviano Manoel", 25, Color.White, new(() => { return 155; }), new(() => { return (scene.Graphics.PreferredBackBufferHeight) - 75; }), Color.White, 0, true),
                new Text("Music sleepwalking by airtone. https://dig.ccmixter.org/files/airtone/65416", 25, Color.White, new(() => { return 365; }), new(() => { return (scene.Graphics.PreferredBackBufferHeight) - 50; }), Color.White, 0, true),
            });


            Button song = new("Music: ON", 25, Color.White, new(() => { return scene.Graphics.PreferredBackBufferWidth - 75; }), new(() => { return 25; }), Color.White, 3, true, 0, 0, new(() => { }));
            song.action = new(() =>
            {
                scene.Play = !scene.Play;
                song.text = $"Music: {scene.issong}";
            });

            EngineButton(new()
            {
                new("Start", 25, Color.White, new(() => { return scene.Graphics.PreferredBackBufferWidth / 2; }), new(() => { return (scene.Graphics.PreferredBackBufferHeight / 2) + 50; }), Color.White, 3, true,0,0, new(() => {scene.SwitchScene(new Hash_Scene());})),
                new("Exit", 25, Color.White, new(() => { return scene.Graphics.PreferredBackBufferWidth / 2; }), new(() => { return (scene.Graphics.PreferredBackBufferHeight / 2) + 100; }), Color.White, 3, true,0,0, new(() => { scene.Exit();})),
                song,
            });


            base.Initialize();
        }
    }
}