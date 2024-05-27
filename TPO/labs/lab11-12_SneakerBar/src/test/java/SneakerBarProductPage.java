import org.openqa.selenium.By;
import org.openqa.selenium.WebDriver;
import org.openqa.selenium.WebElement;

public class SneakerBarProductPage {
    private WebDriver driver;
    By addToFavoritesLocator = By.xpath("//*[@id=\"product\"]/div[4]/div[2]/div/a");
    //By sizeOfProductLocator = By.xpath("/html/body/div[7]/div/div/div[1]/div[2]/div[7]/div[1]/div/div/div[5]/input");
    //By addToBasketLocator = By.xpath("//*[@id=\"button-cart\"]");
    public SneakerBarProductPage(WebDriver driver) {
        this.driver = driver;
    }
    public void addToFavorites(){
        WebElement addToFavoritesButton = driver.findElement(addToFavoritesLocator);
        addToFavoritesButton.click();
    }
    //public void chooseSizeOfProduct(){
    //    driver.findElement(sizeOfProductLocator).click();
    //}
    //public void addProductWithSizeToBasket(){
    //   WebElement addToBasketButton = driver.findElement(addToBasketLocator);
    //    addToBasketButton.click();
    //}
}
