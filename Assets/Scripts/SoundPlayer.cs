using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundPlayer : MonoBehaviour {

    public AudioClip announcerStart, enemy1, enemy2, tower1, tower2, tower3, parrot1, parrot2, boss1, boss2, boss3;
    List<AudioClip> listAll = new List<AudioClip>();

    List<AudioClip> listTower = new List<AudioClip>();

    List<AudioClip> listEnemy = new List<AudioClip>();

    List<AudioClip> listParrot = new List<AudioClip>();

    List<AudioClip> listBoss = new List<AudioClip>();
    List<AudioClip> listET = new List<AudioClip>();



    // Use this for initialization
    void Start () {

        StartCoroutine(Wait());

        AudioPlayer.PlayFile(announcerStart,2f);

        listAll.Add(enemy1);
        listAll.Add(enemy2);
        listAll.Add(tower1);
        listAll.Add(tower2);
        listAll.Add(tower3);
        listAll.Add(parrot1);
        listAll.Add(parrot2);

        listParrot.Add(parrot1);
        listParrot.Add(parrot2);
        listParrot.Add(enemy1);
        listParrot.Add(enemy2);

        listBoss.Add(boss1);
        listBoss.Add(boss2);
        listBoss.Add(boss3);

        listEnemy.Add(enemy1);
        listEnemy.Add(enemy2);

        listTower.Add(tower1);
        listTower.Add(tower2);
        listTower.Add(tower3);

        listET.Add(enemy1);
        listET.Add(enemy2);

        listET.Add(tower1);
        listET.Add(tower2);
        listET.Add(tower3);

    }
	
	// Update is called once per frame
	void Update () {



	}

    private float randomNumber() {
        float number = Random.Range(0f, 1f);
        return number;
    }

    IEnumerator Wait() {
        while (true) {
            yield return new WaitForSeconds(5);
            PlaySound();
        }
        
    }

    void PlaySound() {

        if (GameObject.Find("Boss(Clone)") != null) {
            AudioClip sound = listBoss[Random.Range(0, listBoss.Count-1)];
            Sound(sound);
        } else if (GameObject.Find("Tower(Clone)") != null && GameObject.Find("FlyBois(Clone)") != null) {
            AudioClip sound = listAll[Random.Range(0, listAll.Count-1)];
            Sound(sound);
        } else if (GameObject.Find("FlyBois(Clone)") != null && GameObject.Find("Tower(Clone)") == null) {
            AudioClip sound = listParrot[Random.Range(0, listParrot.Count-1)];
            Sound(sound);
        } else if (GameObject.Find("Tower(Clone)") != null && GameObject.Find("Enemy(Clone)") == null) {
            AudioClip sound = listTower[Random.Range(0, listTower.Count-1)];
            Sound(sound);
        } else if (GameObject.Find("Tower(Clone)") != null && GameObject.Find("Enemy(Clone)") != null) {
            AudioClip sound = listET[Random.Range(0, listET.Count-1)];
            Sound(sound);
        } else if(GameObject.Find("Enemy(Clone)") != null) {
            AudioClip sound = listEnemy[Random.Range(0, listEnemy.Count-1)];
            Sound(sound);
        }
        
    }

    void Sound(AudioClip sound) {
        AudioPlayer.PlayFile(sound, 1f);
    }

}
