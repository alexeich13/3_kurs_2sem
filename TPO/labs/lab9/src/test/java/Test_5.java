import org.junit.Test;
import org.openqa.selenium.WebDriver;
import org.openqa.selenium.edge.EdgeDriver;

public class Test_5 {
    @Test
    public void testFiltering() throws InterruptedException {
        WebDriver driver = new EdgeDriver();
        driver.get("https://sneakers.by/");

        SneakerBarMainPage sneakerBarMainPage = new SneakerBarMainPage(driver);
        SneakerBarLoginPage sneakerBarLoginPage = new SneakerBarLoginPage(driver);
        sneakerBarMainPage.authorizeUser();
        sneakerBarLoginPage.changeToMail();
        sneakerBarLoginPage.enterDataOfUserAndApprove("leha.drozd.99@g,mail.com", "Ld175296");
        Thread.sleep(5000);
        driver.quit();
    }
}
