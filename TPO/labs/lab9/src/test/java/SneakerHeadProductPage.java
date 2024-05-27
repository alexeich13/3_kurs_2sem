import org.openqa.selenium.By;
import org.openqa.selenium.WebDriver;
import org.openqa.selenium.WebElement;

public class SneakerHeadProductPage {
    private WebDriver driver;
    private By addProductToBasketSelector = By.xpath("/html/body/div[3]/div/div[2]/div[2]/form/div[2]/button");

    public SneakerHeadProductPage(WebDriver driver) {
        this.driver = driver;
    }

    public void addProductToBasket() {
        WebElement addProductToBasketButton = driver.findElement(addProductToBasketSelector);
        addProductToBasketButton.click();
    }
}

