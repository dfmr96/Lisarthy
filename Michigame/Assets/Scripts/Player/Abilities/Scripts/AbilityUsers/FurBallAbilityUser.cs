using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FurBallAbilityUser : AbilityUser
{
    private GameObject furBall;
    private float ballSpeed;
    private float ballLifeSpan;


    protected override void ExecuteAtAwake()
    {
        FurBallAbility furBallAbility = ability as FurBallAbility;
        furBall = furBallAbility.furBall;
        ballSpeed = furBallAbility.ballSpeed;
        ballLifeSpan = furBallAbility.ballLifeSpan;

    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //For testing--------------------------------------------------------------------
        FurBallAbility furBallAbility = ability as FurBallAbility;
        furBall = furBallAbility.furBall;
        ballSpeed = furBallAbility.ballSpeed;
        ballLifeSpan = furBallAbility.ballLifeSpan;
//--------------------------------------------------------------------------------------------
        coolDownTime = ability.coolDownTime;
        UpdateCoolDown();

        if (Input.GetKeyDown(abilityKey) && currentCoolDown == 0 && hitBox.activeSelf == false)
        {
            furBall.GetComponent<FurBall>().speed = ballSpeed;
            furBall.GetComponent<FurBall>().damage = damage;
            furBall.GetComponent<FurBall>().lifeSpan = ballLifeSpan;
            hitBox.SetActive(true);
            Shoot();
            hitBox.SetActive(false);
            currentCoolDown = coolDownTime;
        }
    }

    private void Shoot()
    {
        Instantiate(furBall, hitBox.transform.position, hitBox.transform.rotation);
    }


}
