using System.Collections.Generic;
using UnityEngine;

namespace Zombieland.GameScene0.VisualBodyModule
{
    public class GertterTriggers
    {
        private readonly IVisualBodyController _visualBodyController;

        public GertterTriggers(IVisualBodyController visualBodyController)
        {
            _visualBodyController = visualBodyController;
        }

        public List<GameObject> GetSensorTriggers()
        {
            List<GameObject> gameObjects = new List<GameObject>();

            if (_visualBodyController.CharacterInScene != null)
            {
                Transform[] childTransforms = _visualBodyController.CharacterInScene.GetComponentsInChildren<Transform>();

                foreach (Transform child in childTransforms)
                {
                    Collider collider = child.GetComponent<Collider>();
                    if (collider != null && collider.isTrigger)
                    {
                        CharacterJoint characterJoint = child.GetComponent<CharacterJoint>();
                        if (characterJoint != null)
                        {
                            gameObjects.Add(child.gameObject);
                        }
                    }
                }
            }

            return gameObjects;
        }
    }
}