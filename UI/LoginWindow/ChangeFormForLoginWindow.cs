using UnityEngine;

public class ChangeFormForLoginWindow : MonoBehaviour
{
    public GameObject loginForm;
    public GameObject registerForm;
    public GameObject errorForm;
    private void Start()
    {
        registerForm.SetActive(true);
        loginForm.SetActive(false);
        errorForm.SetActive(false);
    }
    public void OnClickToLoginButton()
    {
        loginForm.SetActive(true);
        registerForm.SetActive(false);
    }
    public void OnClickToRegisterButton()
    {
        loginForm.SetActive(false);
        registerForm.SetActive(true);
    }
    public void OnClickToErrorFormButton()
    {
        errorForm.SetActive(false);
    }
}
