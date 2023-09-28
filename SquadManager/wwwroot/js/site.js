function changeMenu(menuToChange) {
    $('nav').fin('li.btn-active').remove();

    if (menuToChange != 'home') {
        $('nav [data-menu="' + menuToChange + '"]').addClass('btn-active')
    }
}