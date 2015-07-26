var Lockscreen = function () {
    var handleLockScreen = function () {
        var IDLE_TIMEOUT = 60; //seconds
        var _idleSecondsTimer = null;
        var _idleSecondsCounter = 0;

        document.onclick = function () {
            _idleSecondsCounter = 0;
        };

        document.onmousemove = function () {
            _idleSecondsCounter = 0;
        };

        document.onkeypress = function () {
            _idleSecondsCounter = 0;
        };

        _idleSecondsTimer = window.setInterval(CheckIdleTime, 1000);

        function CheckIdleTime() {
            _idleSecondsCounter++;
            if (_idleSecondsCounter >= IDLE_TIMEOUT) {
                window.clearInterval(_idleSecondsTimer);
                window.location.href = "http://" + window.location.host + "/lock-screen";
                document.cookie = "lastpath = " + window.location.pathname + "";
            }
        }
    }
    return {
        init: function () {
            handleLockScreen();
        }
    };

}();