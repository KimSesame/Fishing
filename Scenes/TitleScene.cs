namespace Fishing.Scenes
{
    public class TitleScene : Scene
    {
        public TitleScene() { type = SceneType.Title; }

        public override void Enter() { }
        public override void Exit() { }

        public override void Input()
        {
            Console.ReadKey();
        }

        public override void Render()
        {
            Console.Clear();
            Console.WriteLine();
            Console.WriteLine();
            Console.WriteLine();
            Console.WriteLine("     ■■■ ■ ■■■ ■  ■                          ");
            Console.WriteLine("     ■     ■ ■     ■  ■ ■                    ");
            Console.WriteLine("     ■■■ ■ ■■■ ■■■ ■ ■■■ ■■■                 ");
            Console.WriteLine("     ■     ■     ■ ■  ■ ■ ■  ■ ■  ■          ");
            Console.WriteLine("     ■     ■ ■■■ ■  ■ ■ ■  ■ ■■■             ");
            Console.WriteLine("                                           ■");
            Console.WriteLine("                                     ┌ ■  ■");
            Console.WriteLine("                                     │ ■■■");
            Console.WriteLine("                                     │");
            Console.WriteLine("                                     │");
            Console.WriteLine("     Move LEFT or RIGHT              │");
            Console.WriteLine("     Spacebar to start fishing!      │");
            Console.WriteLine("     ESC to quit.                    │");
            Console.WriteLine("                                     │");
            Console.WriteLine("                  ~Press Any Key~    │");
            Console.WriteLine("                                     │");
            Console.WriteLine("        ■                           │");
            Console.WriteLine("        ■■  ■■■                 │");
            Console.WriteLine("        ■■■■■▣■               │");
            Console.WriteLine("        ■■■■■■■               ¿");
            Console.WriteLine("        ■■  ■■■                ");
            Console.WriteLine("        ■                      ");
            Console.WriteLine("                               ");
        }

        public override void Update()
        {
            game.ChangeScene(SceneType.Fishery);
        }
    }
}
