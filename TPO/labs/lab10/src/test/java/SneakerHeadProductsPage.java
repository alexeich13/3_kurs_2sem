import org.openqa.selenium.By;
import org.openqa.selenium.WebDriver;
import org.openqa.selenium.WebElement;

public class SneakerHeadProductsPage {
    private WebDriver driver;
    By openProductSelector = By.xpath("/html/body/div[3]/div/div[3]/div[1]");
    private By openFiltersLocator = By.xpath("/html/body/div[3]/div/a");
    private By insertLowPriceLocator = By.xpath("//*[@id=\"f-price_from\"]");
    private By insertHighPriceLocator = By.xpath("//*[@id=\"f-price_to\"]");
    private By findByOptionsLocator = By.xpath("//*[@id=\"catalog-filter-form\"]/div[1]/div[16]/a[1]");
    public SneakerHeadProductsPage(WebDriver driver) {
        this.driver = driver;
    }
    public void openProductDetails() {
        WebElement openProductClick = driver.findElement(openProductSelector);
        openProductClick.click();
    }
    public void openFilters() {
        WebElement openFiltersButton = driver.findElement(openFiltersLocator);
        openFiltersButton.click();
    }

    public void setPriceRange(String lowPrice, String highPrice) {
        WebElement insertLowPrice = driver.findElement(insertLowPriceLocator);
        insertLowPrice.sendKeys(lowPrice);

        WebElement insertHighPrice = driver.findElement(insertHighPriceLocator);
        insertHighPrice.sendKeys(highPrice);
    }

    public void applyFilters() {
        WebElement findByOptionsButton = driver.findElement(findByOptionsLocator);
        findByOptionsButton.click();
    }
}
