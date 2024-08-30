using UnityEngine;

public static class ShopUtils {
    public static Vector3 CalculateShopItemPosition(int width, int height, Vector3 size, int column, int row, int index) {
        int blockWidth = width / row;
        int blockHeight = height / column;
        
        // Calculate the current row and column based on the index
        int currentRow = index / row;
        int currentColumn = index % row;

        // Calculate the position from the top left corner
        float x = currentColumn * blockWidth + blockWidth / 2 - width / 2;
        float y = height / 2 - currentRow * blockHeight - blockHeight / 2;

        Vector3 pos = new Vector3(x, y, 0);
        
        return pos;
    }
}