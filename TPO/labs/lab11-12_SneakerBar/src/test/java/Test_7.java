import org.junit.Test;
import org.openqa.selenium.WebDriver;
import org.openqa.selenium.edge.EdgeDriver;

public class Test_7 {
    @Test
    public void testFiltering() throws InterruptedException {
        WebDriver driver = new EdgeDriver();
        driver.get("https://sneakers.by/");

        SneakerBarMainPage sneakerBarMainPage = new SneakerBarMainPage(driver);
        SneakerBarLoginPage sneakerBarLoginPage = new SneakerBarLoginPage(driver);
        SneakerBarProductsPage sneakerBarProductsPage = new SneakerBarProductsPage(driver);
        SneakerBarProductPage sneakerBarProductPage = new SneakerBarProductPage(driver);
        sneakerBarMainPage.authorizeUser();
        sneakerBarLoginPage.changeToMail();
        sneakerBarLoginPage.enterDataOfUserAndApprove("leha.drozd.99@g,mail.com", "Ld175296");
        sneakerBarMainPage.openSearch();
        sneakerBarMainPage.insertDataIntoSearch("nike dunk hi retro");
        sneakerBarProductsPage.openProduct();
        sneakerBarProductPage.addToFavorites();
        Thread.sleep(10000);
        driver.quit();
    }
}
