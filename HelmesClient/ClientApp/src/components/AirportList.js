import React from "react";
import Select, { components } from "react-select";

const airportList = [
    {
        value: 1,
        label: "TLL"
    },
    {
        value: 2,
        label: "RIX"
    },
    {
        value: 3,
        label: "HEL"
    }
];

const colourStyles = {
    option: (styles, { data, isDisabled, isFocused, isSelected }) => {
        return {
            ...styles,
            backgroundColor: isFocused ? "#00A3BE" : "",

            color: isFocused ? "#F9FAFC" : "#191D2F",
            display: "flex",
            paddingLeft: 0,

            "& .left": {
                display: "flex",
                justifyContent: "center",
                width: 60,
                marginTop: 3
            },
            "& .right": {
                width: "100%"
            },

            "& .right > .title": {
                display: "block",
                margin: "5px 0"
            }
        };
    }
};

const Option = (props) => {
    return (
        <components.Option {...props}>
            <div className="left">{props.isSelected ? "✔" : ""}</div>
            <div className="right">
                <strong className="title">{props.data.label}</strong>
            </div>
        </components.Option>
    );
};

export default  () => (
        <Select
            defaultValue={airportList[0]}
            label="Single select"
            options={airportList}
            styles={colourStyles}
            components={{
                Option
            }}
        />
);
