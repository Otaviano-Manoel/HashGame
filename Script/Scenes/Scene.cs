using System.Collections.Generic;
using HashGame;
using Microsoft.Xna.Framework;

public class Scene {

    public Manager_Scene scene;

    public void EngineText(List<Text> texts)
    {
        foreach(var t in texts){
            t.Scene = scene;
            scene.texts.Add(t);
        }
    }
    public void EngineButton(List<Button> buttons)
    {
        foreach(var b in buttons){
            b.Scene = scene;
            scene.buttons.Add(b);
        }
    }

    public virtual void Initialize(){}
    public virtual void Update(GameTime gameTime){}
    public virtual void Draw(GameTime gameTime){}
}