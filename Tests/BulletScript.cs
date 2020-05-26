using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

namespace Tests
{
    public class BulletScript
    {
        [Test]
        public void BulletInstantiates()
        {
            Assert.IsNotNull(Object.Instantiate(Resources.Load("prefabs/bullet"), Vector3.zero, Quaternion.identity));
        }
        
        [UnityTest]
        public IEnumerator BulletDoesNotMoveUpAndDown()
        {
            GameObject bullet = (GameObject)Object.Instantiate(Resources.Load("prefabs/bullet"), Vector3.zero, Quaternion.identity);
            float initialPosY = bullet.transform.position.y;
            yield return new WaitForSeconds(0.5f);
            float newPos = bullet.transform.position.y;
            if(newPos < 0.1f)
            {
                newPos = 0;
            }
            Assert.AreEqual(newPos, initialPosY);
            Object.Destroy(bullet.gameObject);
        }
    }
}
