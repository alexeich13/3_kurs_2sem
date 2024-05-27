import org.junit.Test;
import org.openqa.selenium.WebDriver;
import org.openqa.selenium.edge.EdgeDriver;

public class Test_6 {
    @Test
    public void testFiltering() throws InterruptedException {
        WebDriver driver = new EdgeDriver();
        driver.get("https://sneakers.by/");

        SneakerBarMainPage sneakerBarMainPage = new SneakerBarMainPage(driver);
        SneakerBarProductsPage sneakerBarProductsPage = new SneakerBarProductsPage(driver);
        sneakerBarMainPage.openSearch();
        sneakerBarMainPage.insertDataIntoSearch("nike dunk hi retro");
        sneakerBarProductsPage.openProduct();
        Thread.sleep(5000);
        driver.quit();
    }
}
