import org.junit.Test;
import org.openqa.selenium.WebDriver;
import org.openqa.selenium.edge.EdgeDriver;

public class Test_3 {
    @Test
    public void testFiltering() throws InterruptedException {
        WebDriver driver = new EdgeDriver();
        driver.get("https://sneaker-head.by/set-currency?currency=RUB");
        Thread.sleep(5000);
        driver.quit();
    }
}
