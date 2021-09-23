# Burger Builder
Burger Builder with Async Streaming + ReactiveX.

# Description

Trên WinForm có 7 buttons sẽ có name|label lần lượt như sau:
- `buttonMenuI`: **Menu 1**
- `buttonMenuII` **Menu 2**
- `buttonSalad`: **Xà lách**
- `buttonMeat`: **Thịt**
- `buttonTomato`: **Cà chua**
- `buttonCheese`: **Phô mai**
- `buttonSkip`: **Bỏ qua**

**Thực hiện một business logic lần lượt như sau.**

1. Yêu cầu 
	- Nhấp vào Xà lách 2 lần.
	- Nhấp vào Thịt 1 lần.
	- Nhấp vào Cà chua 3 lần.
	- Nhấp vào Phô mai 1 lần.
	- Nhấp vào Thịt 1 lần.
	- Nhấp vào Xà lách 1 lần.

- Kết thúc hiện trình tự cách thành phần của burget: 
`Xà lách - Xà lách - Thịt - Cà chua - Cà chua - Cà chua - Phô mai - Thịt - Xà lách`

*Lưu ý: Đến trình tự nào, thì khi nhấp vào button khác sẽ không có tác dụng*

2. Nếu nhấp vào Menu 1, thực hiện như yêu cầu 1, menu 2 thì ngược lại quy trình.

3. Thực hiện như yêu cầu 1 và 2 nhưng được phép bỏ qua bất kì nguyên liệu nào nếu nhâp vào Bỏ qua.

***Hãy thiết kế và cài đặt ứng dụng trên, theo cách dễ bảo trì, dễ mở rộng và có thể thực hiện unit test được và có thể tái sử dụng component đó cho bất kì ứng dụng khác như WPF/XF/MAUI/UWP....***