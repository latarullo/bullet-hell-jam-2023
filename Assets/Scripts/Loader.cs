using UnityEngine.SceneManagement;

public static class Loader {

    public enum Scene {
        MainMenuScene,
        GameScene,
        LoadingScene,
    }

    private static Scene targetScene;

    public static void LoadScene(Scene scene) {
        targetScene = scene;
        SceneManager.LoadScene(Scene.LoadingScene.ToString());
    }

    public static void LoaderCallBack() {
        SceneManager.LoadScene(targetScene.ToString());
    }
}
