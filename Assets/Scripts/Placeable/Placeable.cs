using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace TD
{
    // Base class for all objects that can be placed
    public class Placeable : MonoBehaviour
    {
        public PlaceableType pType;

        [HideInInspector] public Faction faction;
        [HideInInspector] public PlaceableTarget targetType;
        [HideInInspector] public AudioClip dieAudioClip;

        public UnityAction<Placeable> onDie;

        public enum PlaceableType
        {
            Tower,
            Barrack,
            Unit,
        }

        public enum PlaceableTarget
        {
            Norm,
            Camo,
            Both,
            None,
        }

        public enum Faction
        {
            Player,
            UFO,
        }
    }
}

