import org.openqa.selenium.By;
import org.openqa.selenium.WebDriver;
import org.openqa.selenium.WebElement;

public class SneakerBarProductsPage {
    private WebDriver driver;
    By openProductLocator = By.xpath("//*[@id=\"mfilter-content-container\"]/div[2]/div/div");
    By filterByBrend = By.xpath("//*[@id=\"mfilter-box-87\"]/div[2]/ul/li[3]/div[2]/div[1]/div[1]/div/div/div/div[1]/label");
    By filterBySize = By.xpath("//*[@id=\"mfilter-box-87\"]/div[2]/ul/li[4]/div[2]/div[1]/div[1]/div/div/div/div[21]/label");
    public SneakerBarProductsPage(WebDriver driver) {
        this.driver = driver;
    }
    public void openProduct(){
        driver.get("https://sneakers.by/katalog/obuv-belarus/kupit-krossovki-v-belarusi/krossovki-nike-dunk-hi-retro-se-dv7223-800");
    }
    public void chooseFilters(){
        WebElement filterBrand  = driver.findElement(filterByBrend);
        WebElement filterSize  = driver.findElement(filterBySize);
        filterBrand.click();
        filterSize.click();
    }
}
