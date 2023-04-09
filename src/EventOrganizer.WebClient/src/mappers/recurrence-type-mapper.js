import recurrenceTypes from 'constants/recurrence-types'

export const mapStringToEnum = (type) => {
    switch (type) {
        case recurrenceTypes.doesNotRepeat:
            return 0;
        case recurrenceTypes.dayly:
            return 1;
        case recurrenceTypes.everyWeekDay:
            return 2;
        case recurrenceTypes.weekly:
            return 3;
        case recurrenceTypes.monthly:
            return 4;
        case recurrenceTypes.yearly:
            return 5;
        default:
            return 0;
    }
}

export const mapEnumToString = (num) => {
    switch (num) {
        case 0:
            return recurrenceTypes.doesNotRepeat;
        case 1:
            return recurrenceTypes.dayly;
        case 2:
            return recurrenceTypes.everyWeekDay;
        case 3:
            return recurrenceTypes.weekly;
        case 4:
            return recurrenceTypes.monthly;
        case 5:
            return recurrenceTypes.yearly;
        default:
            return recurrenceTypes.doesNotRepeat;
    }
}

const recurrenceTypeMapper = {
    mapStringToEnum,
    mapEnumToString
}

export default recurrenceTypeMapper;