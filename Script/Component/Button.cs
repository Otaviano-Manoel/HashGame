using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

public class Button : Text, IUpdate
{
    public Action action;

    private bool clickCaptured;


    public Button(string text, int size, Color tColor, PositionX x, PositionY y, Color rColor, float thickness, bool isMeassureString, int width, int height, Action action) : base(text, size, tColor, x, y, rColor, thickness, isMeassureString, width, height)
    {
        this.action = action;
    }

    public void Update(GameTime gameTime)
    {
        MouseState mouseState = Mouse.GetState();

        if(!field.Contains(mouseState.Position)) return;


        if (mouseState.LeftButton == ButtonState.Pressed && !clickCaptured)
        {
            // processar clique aqui
            action();
            clickCaptured = true;
        }
        else if (mouseState.LeftButton == ButtonState.Released)
        {
            clickCaptured = false;
        }
    }

    public delegate void Action();
}