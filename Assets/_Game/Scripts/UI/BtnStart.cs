using UnityEngine;

public class BtnStart : ButtonBase
{
    private void Start()
    {
        button.onClick.AddListener(Click);
    }

    public void Click()
    {
        GameManage.Ins.StartGame();
    }
}
