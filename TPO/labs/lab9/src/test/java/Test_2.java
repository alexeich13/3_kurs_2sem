import org.junit.Test;
import org.openqa.selenium.WebDriver;
import org.openqa.selenium.edge.EdgeDriver;

public class Test_2 {
    @Test
    public void testFiltering() throws InterruptedException {
        WebDriver driver = new EdgeDriver();
        driver.get("https://sneaker-head.by/");

        SneakerHeadMainPage sneakerMainPage = new SneakerHeadMainPage(driver);
        SneakerHeadProductsPage sneakerProductsPage = new SneakerHeadProductsPage(driver);
        sneakerMainPage.moveToMenSection();
        sneakerProductsPage.openFilters();
        sneakerProductsPage.setPriceRange("200", "600");
        sneakerProductsPage.applyFilters();

        Thread.sleep(5000);
        driver.quit();
    }
}
