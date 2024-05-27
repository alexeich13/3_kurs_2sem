import org.openqa.selenium.By;
import org.openqa.selenium.WebDriver;
import org.openqa.selenium.WebElement;

public class SneakerBarFavoritesPage {
    private WebDriver driver;
    By openFavoritesLocator = By.xpath("//*[@id=\"wishlist\"]/button");
    By deleteFromFavoritesLocator = By.xpath("//*[@id=\"content\"]/div[1]/table/tbody/tr/td[6]/a");
    //By addToBasketLocator = By.xpath("//*[@id=\"content\"]/div[1]/table/tbody/tr/td[6]/button");
    public SneakerBarFavoritesPage(WebDriver driver) {
        this.driver = driver;
    }
    public void openFavorites(){
        WebElement openFavoritesButton = driver.findElement(openFavoritesLocator);
        openFavoritesButton.click();
    }
    public void deleteFromFavorites(){
        WebElement deleteFromFavoritesButton = driver.findElement(deleteFromFavoritesLocator);
        deleteFromFavoritesButton.click();
    }
}
