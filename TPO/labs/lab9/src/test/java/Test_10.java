import org.junit.Test;
import org.openqa.selenium.WebDriver;
import org.openqa.selenium.edge.EdgeDriver;

public class Test_10 {
    @Test
    public void testFiltering() throws InterruptedException {
        WebDriver driver = new EdgeDriver();
        driver.get("https://sneakers.by/");

        SneakerBarMainPage sneakerBarMainPage = new SneakerBarMainPage(driver);
        SneakerBarProductsPage sneakerBarProductsPage = new SneakerBarProductsPage(driver);
        sneakerBarMainPage.moveToMen();
        sneakerBarProductsPage.chooseFilters();
        Thread.sleep(5000);
        driver.quit();
    }
}
