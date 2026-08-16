define(["dojo/_base/declare", "epi/shell/widget/PropertyEditor"],
    function (declare, PropertyEditor) {
        return declare([PropertyEditor], {
            postCreate: function () {
                this.inherited(arguments);

                // Hide editor for all cells except the first header cell
                if (!this._metadata || this._metadata.index !== 0) {
                    this.domNode.style.display = "none";
                }
            }
        });
    });
