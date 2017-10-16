using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundPlayer : MonoBehaviour {

    public AudioClip announcerStart, enemy1, enemy2, enemy3, tower1, tower2, tower3;
    List<AudioClip> listAll = new List<AudioClip>();

    List<AudioClip> listTower = new List<AudioClip>();

    List<AudioClip> listEnemy = new List<AudioClip>();

	// Use this for initialization
	void Start () {

        Sound(announcerStart);

        listAll.Add(enemy1);
        listAll.Add(enemy2);
        listAll.Add(enemy3);
        listAll.Add(tower1);
        listAll.Add(tower2);
        listAll.Add(tower3);

        listEnemy.Add(enemy1);
        listEnemy.Add(enemy2);
        listEnemy.Add(enemy3);

        listTower.Add(tower1);
        listTower.Add(tower2);
        listTower.Add(tower3);
    }
	
	// Update is called once per frame
	void Update () {

        StartCoroutine(Wait());

	}

    private float randomNumber() {
        float number = Random.Range(0f, 1f);
        return number;
    }

    IEnumerator Wait() {
        yield return new WaitForSeconds(1);
        PlaySound();
    }

    void PlaySound() {
       // if(randomNumber() <= 1) {
            if(GameObject.Find("Enemy(Clone)") != null && GameObject.Find("Tower(Clone)") != null) {
                AudioClip sound = listAll[Random.Range(0, listAll.Count)];
                Sound(sound);
            } else if(GameObject.Find("Enemy(Clone)") != null) {
                AudioClip sound = listEnemy[Random.Range(0, listAll.Count)];
                Sound(sound);
            } else if (GameObject.Find("Tower(Clone)") != null) {
                AudioClip sound = listTower[Random.Range(0, listAll.Count)];
                Sound(sound);
            }
        //}
    }

    void Sound(AudioClip sound) {
        AudioPlayer.PlayFile(sound, 1f);
    }

}
