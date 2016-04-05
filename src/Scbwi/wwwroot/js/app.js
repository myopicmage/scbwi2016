Array.prototype.single = function (func) {
    var temp = this.filter(func);

    if (temp.length === 1) {
        return temp[0];
    } else {
        return null;
    }
};

var app = angular.module('Scbwi', ['ngMaterial']);

app.config(function ($mdThemingProvider) {
    $mdThemingProvider.definePalette('amazingPaletteName', {
        '50': 'ffebee',
        '100': 'ffcdd2',
        '200': 'ef9a9a',
        '300': 'e57373',
        '400': 'ef5350',
        '500': '03a9f4', //main
        '600': 'e53935',
        '700': 'd32f2f',
        '800': '0277bd', //md-hue-2
        '900': 'b71c1c',
        'A100': 'ff8a80',
        'A200': 'ff5252',
        'A400': 'ff1744',
        'A700': 'd50000',
        'contrastDefaultColor': 'light',    // whether, by default, text (contrast)
        // on this palette should be dark or light
        'contrastDarkColors': ['50', '100', //hues which contrast should be 'dark' by default
         '200', '300', '400', 'A100'],
        'contrastLightColors': undefined    // could also specify this if default was 'dark'
    });
    $mdThemingProvider.theme('default')
      .primaryPalette('amazingPaletteName')
});

app.controller('AppCtrl', function () {
    var self = this;

    self.go = function (url) {
        window.location = url;
    };
});

app.controller('RegCtrl', function ($http, $scope) {

});

app.controller('DashboardCtrl', function () {
    var self = this;
});

app.controller('RegistrationController', function ($http, $scope) {
    var self = this;

    self.reg = {};
    self.packages = [];
    self.isMember = false;
    self.showTracks = false;
    self.memberBox = true;
    self.showPackages = false;
    self.timetopay = false;

    self.packageSelect = function () {
        self.showTracks = true;
    };

    self.yes = function () {
        self.memberBox = false;
        self.isMember = true;
        self.showPackages = true;
    };

    self.no = function () {
        console.log("no!");
        self.memberBox = false;
        self.isMember = false;
        self.showPackages = true;
    };

    self.getViewModel = function () {
        return {
            first: self.reg.first,
            last: self.reg.last,
            address1: self.reg.address1,
            address2: self.reg.address2,
            city: self.reg.city,
            state: self.reg.state,
            zip: self.reg.zip,
            country: self.reg.country,
            email: self.reg.email,
            phone: self.reg.phone,
            packageid: self.reg.package,
            comprehensiveid: self.reg.comprehensive,
            trackid: self.reg.track,
            couponid: self.reg.coupon,
            manuscripts: self.reg.manuscript,
            portfolio: self.reg.portfolio
        };
    };

    self.calculate = function () {
        $http({
            method: 'post',
            url: '/info/calculatetotal',
            data: self.getViewModel()
        }).then(function (data) {
            self.reg.total = data.data.total;
            self.reg.subtotal = data.data.subtotal;
            self.hasTotal = true;
            self.allowSubmit = true;
        });
    };

    self.submit = function () {
        $http({
            method: 'post',
            url: '/info/submit',
            data: self.getViewModel()
        }).then(function (data) {
            self.paypal = data.data;
            self.timetopay = true;
        });
    };

    $http({
        method: 'post',
        url: '/info/getpackages'
    }).then(function (data) {
        self.packages = data.data;
    });

    $http({
        method: 'get',
        url: '/info/gettracks'
    }).then(function (data) {
        self.tracks = data.data;
    });

    $http({
        method: 'get',
        url: '/info/getcomprehensives'
    }).then(function (data) {
        self.comprehensives = data.data;
    });
});

app.controller('PackageController', function ($http) {
    var self = this;

    self.packages = [];

    self.getPackages = function () {
        $http({
            method: 'post',
            url: '/admin/getpackages'
        }).then(function (data) {
            console.log(data.data);
            self.packages = data.data;
        });
    };

    self.Submit = function () {
        console.log(self.newpackage);

        $http({
            method: 'post',
            url: '/admin/addpackage',
            data: {
                title: self.newpackage.title,
                description: self.newpackage.description,
                regularprice: self.newpackage.regprice,
                lateprice: self.newpackage.lateprice,
                member: self.newpackage.member === true,
                max: self.newpackage.maxattendees
            }
        })
        .then(function (data) {
            self.newpackage = {};
            self.getPackages();
        }, function (data) {
            console.log(data);
        });
    };

    self.getPackages();
});

app.controller('TrackController', function ($http) {
    var self = this;

    self.tracks = [];

    self.getTracks = function () {
        $http({
            method: 'post',
            url: '/info/gettracks'
        }).then(function (data) {
            console.log(data.data);
            self.tracks = data.data;
        });
    };

    self.Submit = function () {
        console.log(self.newpackage);

        $http({
            method: 'post',
            url: '/admin/addtrack',
            data: {
                title: self.newtrack.title,
                description: self.newtrack.description,
                presenters: self.newtrack.presenters
            }
        })
        .then(function (data) {
            self.newtrack = {};
            self.getPackages();
        }, function (data) {
            console.log(data);
        });
    };

    self.getTracks();
});

app.controller('ComprehensiveController', function ($http) {
    var self = this;

    self.comprehensives = [];

    self.getComprehensives = function () {
        $http({
            method: 'post',
            url: '/admin/getcomprehensives'
        }).then(function (data) {
            self.comprehensives = data.data;
        });
    };

    self.Submit = function () {
        console.log(self.newpackage);

        $http({
            method: 'post',
            url: '/admin/addcomprehensive',
            data: {
                title: self.newc.title,
                description: self.newc.description,
                presenters: self.newc.presenters,
                maxattendees: self.newc.maxattendees,
                price: self.newc.price
            }
        })
        .then(function (data) {
            self.newc = {};
            self.getComprehensives();
        }, function (data) {
            console.log(data);
        });
    };

    self.getComprehensives();
});

app.controller('CouponController', function ($http) {
    var self = this;

    self.coupons = [];

    self.getCoupons = function () {
        $http({
            method: 'post',
            url: '/admin/getcoupons'
        }).then(function (data) {
            self.coupons = data.data;
        });
    };

    self.Submit = function () {
        $http({
            method: 'post',
            url: '/admin/addcoupon',
            data: {
                type: main.newcoupon.type,
                value: main.newcoupon.value,
                text: main.newcoupon.text
            }
        })
        .then(function (data) {
            self.newcoupon = {};
            self.getCoupons();
        }, function (data) {
            console.log(data);
        });
    };

    self.getCoupons();
});

app.controller('DateController', function ($http) {
    var self = this;

    self.dates = [];

    self.getDates = function () {
        $http({
            method: 'post',
            url: '/admin/getdates'
        }).then(function (data) {
            self.dates = data.data;
        });
    };

    self.Submit = function () {
        $http({
            method: 'post',
            url: '/admin/adddate',
            data: {
                type: main.newcoupon.type,
                value: main.newcoupon.value,
                text: main.newcoupon.text
            }
        })
        .then(function (data) {
            self.newdate = {};
            self.getDates();
        }, function (data) {
            console.log(data);
        });
    };

    self.getDates();
});
