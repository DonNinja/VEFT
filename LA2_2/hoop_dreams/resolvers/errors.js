const { ApolloError, UserInputError } = require('apollo-server');

class PickupGameExceedMaximumError extends ApolloError {
    constructor(message = 'Pickup game has exceeded the maximum of players.') {
        super(message, null, null);
        // super(null, null, message);
        this.name = 'PickupGameExceedMaximumError';
        this.code = 409;
    }
};

class BasketballFieldClosedError extends ApolloError {
    constructor(message = 'Cannot add a pickup game to a closed basketball field') {
        super(message, null, null);
        // super(null, null, message);
        this.name = 'BasketballFieldClosedError';
        this.code = 400;
    }
};

class PickupGameOverlapError extends ApolloError {
    constructor(message = 'Pickup games cannot overlap') {
        super(message, null, null);
        // super(null, null, message);
        this.name = 'PickupGameOverlapError';
        this.code = 400;
    }
};

class PickupGameAlreadyPassedError extends ApolloError {
    constructor(message = 'Pickup game has already passed') {
        super(message, null, null);
        // super(null, null, message);
        this.name = 'PickupGameAlreadyPassedError';
        this.code = 400;
    }
}

class NotFoundError extends ApolloError {
    constructor(message = 'Id was not found') {
        super(message, null, null);
        // super(null, null, message);
        this.name = 'NotFoundError';
        this.code = 404;
    }
}

class EndBeforeStartError extends ApolloError {
    constructor(message = 'End date cannot be before start date') {
        super(message, null, null);
        this.name = 'EndBeforeStartError';
        this.code = 400;
    }
}

class DateBeforeCurrentDateError extends ApolloError {
    constructor(message = 'Date given is positioned before the current date') {
        super(message, null, null);
        this.name = 'DateBeforeCurrentDate';
        this.code = 400;
    }
}

class InvalidTimeLengthError extends ApolloError {
    constructor(message = 'Pickupgame can be minimum 5 minutes, maximum 2 hours') {
        super(message, null, null);
        this.name = 'InvalidTimeLengthError';
        this.code = 400;
    }
}

class PlayerAlreadyPlayingError extends ApolloError {
    constructor(message = 'Player can only be registered once to the same pickup game') {
        super(message, null, null);
        this.name = 'PlayerAlreadyPlayingError';
        this.code = 400;
    }
}

class PlayerNotPlayingError extends ApolloError {
    constructor(message = 'Player is not playing in this game') {
        super(message, null, null);
        this.name = 'PlayerNotPlayingError';
        this.code = 400;
    }
}

module.exports = {
    PickupGameExceedMaximumError,
    BasketballFieldClosedError,
    PickupGameOverlapError,
    PickupGameAlreadyPassedError,
    NotFoundError,
    UserInputError,
    EndBeforeStartError,
    DateBeforeCurrentDateError,
    InvalidTimeLengthError,
    PlayerAlreadyPlayingError,
    PlayerNotPlayingError
};
