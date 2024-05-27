import org.junit.Test;
import org.openqa.selenium.WebDriver;
import org.openqa.selenium.edge.EdgeDriver;

public class Test_4 {
    @Test
    public void testFiltering() throws InterruptedException {
        WebDriver driver = new EdgeDriver();
        driver.get("https://sneaker-head.by/");
        SneakerHeadMainPage sneakerMainPage = new SneakerHeadMainPage(driver);
        sneakerMainPage.openProductWithDetails();
        Thread.sleep(5000);
        driver.quit();
    }
}
