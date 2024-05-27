import org.openqa.selenium.By;
import org.openqa.selenium.WebDriver;
import org.openqa.selenium.WebElement;

public class SneakerBarLoginPage {
    private WebDriver driver;
    By changeVariantOfEnterLocator = By.xpath("//*[@id=\"sms_form\"]/table/tbody/tr/td[3]");
    By enterLoginLocator = By.xpath("//*[@id=\"input-email\"]");
    By enterPasswordLocator = By.xpath("//*[@id=\"input-password\"]");
    By approveButtonLocator = By.xpath("//*[@id=\"auth_email\"]/form/table/tbody/tr/td[1]/input");
    public SneakerBarLoginPage(WebDriver driver) {
        this.driver = driver;
    }
    public void changeToMail(){
        WebElement changeVariantButton = driver.findElement(changeVariantOfEnterLocator);
        changeVariantButton.click();
    }
    public void enterDataOfUserAndApprove(String login, String password){
        WebElement enterLogin = driver.findElement(enterLoginLocator);
        WebElement enterPassword = driver.findElement(enterPasswordLocator);
        WebElement approveButton = driver.findElement(approveButtonLocator);
        enterLogin.sendKeys(login);
        enterPassword.sendKeys(password);
        approveButton.click();
    }
}
