import org.openqa.selenium.*;

public class SneakerBarMainPage {
    private WebDriver driver;
    By loginPageLocator = By.xpath("//*[@id=\"account\"]/button");
    By searchOpenLocator = By.xpath("//*[@id=\"search\"]/button");
    By insertDataLocator = By.xpath("//*[@id=\"search\"]/ul/li/input");
    By moveToMenLocator = By.xpath("//*[@id=\"horizontal-menu\"]/div[2]/ul/li[6]/a");

    public SneakerBarMainPage(WebDriver driver) {
        this.driver = driver;
    }

    public void authorizeUser(){
        WebElement loginButton = driver.findElement(loginPageLocator);
        loginButton.click();
    }

    public void openSearch(){
        WebElement openSearch = driver.findElement(searchOpenLocator);
        openSearch.click();
    }

    public void insertDataIntoSearch(String product){
        WebElement searchInput = driver.findElement(insertDataLocator);
        searchInput.sendKeys(product);
        searchInput.sendKeys(Keys.ENTER);
    }
    public void moveToMen(){
        WebElement menButton = driver.findElement(moveToMenLocator);
        menButton.click();
    }

}