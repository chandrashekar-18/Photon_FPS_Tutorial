using UnityEngine;

public class MenuManager : MonoBehaviour
{
    [SerializeField] private Menu[] menus;

    public void OpenMenu(string menuName)
    {
        foreach (var menu in menus)
        {
            if (menu.menuName == menuName)
                menu.Open();
            else if (menu.isOpen)
                CloseMenu(menu);
        }
    }

    private void OpenMenu(Menu menuToOpen)
    {
        foreach (var menu in menus)
        {
            if (menu.isOpen)
                CloseMenu(menu);
        }

        menuToOpen.Open();
    }
    
    private void CloseMenu(Menu menu)
    {
        menu.Close();
    }
}
