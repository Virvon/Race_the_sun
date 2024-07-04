using Cysharp.Threading.Tasks;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using Zenject;

namespace Assets.RaceTheSun.Sources.UI
{
    public class UiRoot : MonoBehaviour
    {
        public class Factory : PlaceholderFactory<string, UniTask<UiRoot>>
        {
        }
    }
}
