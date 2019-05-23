var baseUrl = "https://localhost:44366/";

window.onload = function () {

	//$.ajax({
	//	url: baseUrl + 'Home/LogIn',
	//	success: function (data) {
	//		$("#userSection").html(data);
	//	},
	//	error: function (err) {
	//		alert("Något gick fel");
	//	}
	//});

	//$.ajax({
	//	url: baseUrl + 'Auctions/CreateAuction',
	//	success: function (data) {
	//		$("#actionSection").html(data);
	//	},
	//	error: function (err) {
	//		alert("Något gick fel");
	//	}
	//});

	$.ajax({
		url: baseUrl + 'Auctions/Search',
		success: function (data) {
			$("#searchField").html(data);
		},
		error: function (err) {
			alert("Något gick fel");
		}
	});
};


//function Register() {

//	$('#userSection').modal('hide');

//	$.ajax({
//		url: baseUrl + 'Home/RegisterUser',
//		success: function (data) {
//			$("#register").html(data);
//		},
//		error: function (err) {
//			alert("Något gick fel");
//		}
//	});
//}

//function LostPassword() {
//	$('#userSection').modal('hide');

//	$.ajax({
//		url: baseUrl + 'Home/ForgotPassword',
//		success: function (data) {
//			console.log(data);
//			$("#lostPassword").html(data);
//		},
//		error: function (err) {
//			alert("Något gick fel");
//		}
//	});
//}
