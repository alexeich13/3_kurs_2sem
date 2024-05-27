import org.junit.Test;
import org.openqa.selenium.WebDriver;
import org.openqa.selenium.edge.EdgeDriver;

public class Test_1 {
    @Test
    public void testSneakerHead() throws InterruptedException {
        WebDriver driver = new EdgeDriver();
        driver.get("https://sneaker-head.by/");

        SneakerHeadMainPage sneakerHeadPage = new SneakerHeadMainPage(driver);
        SneakerHeadProductsPage sneakerProductsPage = new SneakerHeadProductsPage(driver);
        SneakerHeadProductPage sneakerProductPage = new SneakerHeadProductPage(driver);
        sneakerHeadPage.openSearch();
        sneakerHeadPage.searchForProduct("Adidas Campus");
        sneakerProductsPage.openProductDetails();
        sneakerProductPage.addProductToBasket();
        Thread.sleep(5000);
        driver.quit();
    }
}
