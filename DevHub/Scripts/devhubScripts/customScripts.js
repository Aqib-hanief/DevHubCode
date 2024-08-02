$(document).ready(function () {

    //for darkmode
    if (localStorage.getItem('dark') === 'dark') {
        $('body').addClass(localStorage.getItem('dark'));
        $('.toggle').addClass('active');
        $('.tooltiptext').addClass('lightmode');
        $('.tooltiptext').text("Light Mode");
        $('.container').addClass('text-white');
        $('.table th').addClass('text-dark');

    }
    else if (localStorage.getItem('light') === 'light') {
        $('body').addClass(localStorage.getItem('light'));
        $('.toggle').removeClass('active');
        $('.tooltiptext').removeClass('lightmode');
        $('.tooltiptext').text("Dark Mode");
    }

    $('.toggle').click(function () {
        $('.toggle').toggleClass('active');

        $('body').toggleClass('dark');
        if ($('body').hasClass('dark')) {
            localStorage.removeItem('light');
            localStorage.setItem("dark", "dark");
           

        }
        else {
            localStorage.removeItem('dark');
            localStorage.setItem('light', 'light');
        }
        
        $('.tooltiptext').toggleClass('lightmode');
        if ($('.tooltiptext').hasClass('lightmode')) {
            $('.tooltiptext').text("Light Mode");
        }
        else {
            $('.tooltiptext').text("Dark Mode");
        }
        $('.post').removeClass('bg-white');
        $('.post').toggleClass('bg-dark');
        $('.table th').toggleClass('text-dark');
        $('.container').toggleClass('text-white');
    });

    let socialGroup = $('#socialGroup');
    let reveal = $('#reveal');
    socialGroup.hide();

    //option social inputs in profile tab
    $('#socialLinks').click(function () {
        socialGroup.show();
    });

    //For hiding and unhiding password
    $(reveal).click(function () {
        if (reveal.hasClass('fas fa-eye')) {
            reveal.removeClass('fas fa-eye');
            reveal.addClass('far fa-eye-slash');
            $('#Password').attr('type', 'password');
        }
        else if (reveal.hasClass('far fa-eye-slash')) {
            reveal.removeClass('far fa-eye-slash');
            reveal.addClass('fas fa-eye');
            $('#Password').attr('type', 'text');
        }
        
    });

    
});



// Getting social links on view profile page

function getSocialLinks() {
    $.ajax({
        url: '/Profile/GetSocialLinks',
        method: 'get',
        success: function (socialLinks) {
            $(socialLinks).each(function (i, socialLink) {
                let link = socialLink.Link;
                let site = link.substr(0, link.indexOf('.'));
                let username = link.substr(link.indexOf('/')+1);

                if (site === 'facebook') {
                    $('#facebook').attr('href', 'http://' + socialLink.Link);
                    $('[name="Facebook"]').val(username);
                }
                else if (site === 'twitter') {
                    $('#twitter').attr('href', 'http://' +socialLink.Link);
                    $('[name="Twitter"]').val(username);
                }
                else if (site === 'youtube') {
                    $('#youtube').attr('href', 'http://' +socialLink.Link);
                    $('[name="Youtube"]').val(username);
                }
                else if (site === 'linkedin') {
                    $('#linkedin').attr('href', 'http://' +socialLink.Link);
                    $('[name="Linkedin"]').val(username);
                }
                else if (site === 'instagram') {
                    $('#instagram').attr('href', 'http://' +socialLink.Link);
                    $('[name="Instagram"]').val(username);
                }
            });
        },
        error: function (xhr) {
            console.log(xhr.responseText);
        }
    });
}

//updating social links
function updateSocialLinks() {
    let serializedData = $('#socialLinksForm').serializeArray();
    $.ajax({
        url: '/Profile/UpdateSocialLinks',
        method: 'post',
        data: serializedData,
        success: function () {
            console.log(serializedData);
            $('#facebook').attr('href', 'http://facebook.com/' + serializedData[1].value);

            
            $('#twitter').attr('href', 'http://twitter.com/' + serializedData[0].value);
                   
                
            $('#youtube').attr('href', 'http://youtube.com/' + serializedData[2].value);
                   
               
            $('#linkedin').attr('href', 'http://linkedin.com' + serializedData[3].value);
                   
            $('#instagram').attr('href', 'http://instagram.com' + serializedData[4].value);

        },
        error: function (xhr) {
            console.log(xhr.responseText);
        }

    });
}

// ===== Scroll to Top ==== 
$(window).scroll(function () {
    if ($(this).scrollTop() >= 50) {        // If page is scrolled more than 50px
        $('#return-to-top').fadeIn(200);    // Fade in the arrow
    } else {
        $('#return-to-top').fadeOut(200);   // Else fade out the arrow
    }
});
$('#return-to-top').click(function () {      // When arrow is clicked
    $('body,html').animate({
        scrollTop: 0                       // Scroll to top of body
    }, 500);
});







