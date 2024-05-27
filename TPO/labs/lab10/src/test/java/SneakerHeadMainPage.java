import org.openqa.selenium.By;
import org.openqa.selenium.Keys;
import org.openqa.selenium.WebDriver;
import org.openqa.selenium.WebElement;

public class SneakerHeadMainPage {
    private WebDriver driver;
    By openSearchLocator = By.xpath("/html/body/header/div/div/div[5]/ul/li[2]/a/img");
    By insertDataLocator = By.xpath("//*[@id=\"search-input\"]");
    By productDetailsLocator = By.xpath("/html/body/div[5]/div/div[2]/div[1]/a[1]");
    private By moveToMenLocator = By.xpath("/html/body/div[3]/div/div/div/div/div/div/div[1]/a");

    public SneakerHeadMainPage(WebDriver driver) {
        this.driver = driver;
    }

    public void openSearch() {
        WebElement openSearchButton = driver.findElement(openSearchLocator);
        openSearchButton.click();
    }

    public void searchForProduct(String productName) {
        WebElement insertData = driver.findElement(insertDataLocator);
        insertData.sendKeys(productName);
        insertData.sendKeys(Keys.ENTER);
    }
    public void moveToMenSection() {
        WebElement moveToMenLocatorButton = driver.findElement(moveToMenLocator);
        moveToMenLocatorButton.click();
    }
    public void openProductWithDetails() {
        WebElement openProductClick = driver.findElement(productDetailsLocator);
        openProductClick.click();
    }
}


