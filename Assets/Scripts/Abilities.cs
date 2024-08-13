using UnityEngine;
using UnityEngine.UI;

public class Abilities : MonoBehaviour
{

    [Header("DashIcon")]
    public Image dashImage;
    public float dashCool = 2;
    bool isDashCooldown = false;
    public KeyCode DashIcon;

    [Header("SuperJumpIcon")]
    public Image superJumpImage;
    public float superJumpCool = 2;
    bool isSuperJumpCooldown = false;
    public KeyCode SuperJumpIcon;

    void Start()
    {
        dashImage.fillAmount = 0;
        superJumpImage.fillAmount = 0;
    }

    void Update()
    {
        DashCoolDown();
        SuperJumpCoolDown();
    }

    void DashCoolDown()
    {
        if (Input.GetKey(DashIcon) && isDashCooldown == false)
        {
            isDashCooldown = true;
            dashImage.fillAmount = 1;
        }

        if (isDashCooldown)
        {
            dashImage.fillAmount -= 1 / dashCool * Time.deltaTime;

            if (dashImage.fillAmount <= 0)
            {
                dashImage.fillAmount = 0;
                isDashCooldown = false;
            }
        }
    }

    void SuperJumpCoolDown()
    {
        if (Input.GetKey(SuperJumpIcon) && isSuperJumpCooldown == false)
        {
            isSuperJumpCooldown = true;
            superJumpImage.fillAmount = 1;
        }

        if (isSuperJumpCooldown)
        {
            superJumpImage.fillAmount -= 1 / superJumpCool * Time.deltaTime;

            if (superJumpImage.fillAmount <= 0)
            {
                superJumpImage.fillAmount = 0;
                isSuperJumpCooldown = false;
            }
        }
    }
}
