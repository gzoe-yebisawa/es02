/// <summary>
/// Persistent
/// </summary>
using UnityEngine;

namespace Gzoe
{
    /// <summary>
    /// Persistent
    /// DontDestroyを有効にするだけ
    /// </summary>
    public class Persistent : MonoBehaviour
    {
        private void Awake()
        {
            DontDestroyOnLoad(this);
        }
    }
}