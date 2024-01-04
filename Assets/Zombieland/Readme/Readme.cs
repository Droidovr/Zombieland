namespace Zombieland
{
    public class Readme
    {
/*
    Добавление контроллера на примере CharacterController:
    - 0 Все контроллеры должны наследоваться от абстрактного класса Controller и реализовать свой интерфейс (ICharacterController)
    - 1 в интерфейсе порождающего контроллера IRootController добавить саойство ICharacterController CharacterController { get; }
    - 2 в классе RootController реализовать public ICharacterController CharacterController { get; private set; }
    - 3 в классе RootController в методе
    protected override void CreateSubsystems(ref List<IController> subsystemsControllers)
        {
            CharacterController = new CharacterController(this);
            subsystemsControllers.Add((IController)CharacterController);
        }
        создать экземпляр контроллера и добавить его в список контроллеров подсистем. В конструктор каждого контроллера в качестве аргумента передаём ссылку на порождающий контроллер
 */
    }
}