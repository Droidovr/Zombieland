using Zombieland.GameScene0.NPCManagerModule;
using Zombieland.GameScene0.NPCModule.NPCDataModule;

namespace Zombieland.GameScene0.NPCModule
{
    public interface INPCController
    {
        INPCManagerController NPCManagerController { get; }
        NPCSpawnData NPCSpawnData { get; }
        INPCDataController NPCDataController { get; }
    }
}