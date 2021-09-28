const { Area, Shark, Attack, connection } = require("./data/db");
const {
	TIGER_SHARK,
	HAMMERHEAD_SHARK,
	GREAT_WHITE_SHARK,
	BULL_SHARK,
} = require("./constants");

// 1.1. Get all sharks
const getAllSharks = async () => {
	return Shark.find({})
		.exec()
		.then((data) => {
            return data;
		})
		.catch((err) => {
			console.log(err)
		});
};

// 1.2. Get all tiger sharks
const getTigerSharks = async () => {
	return Shark.find({ species: TIGER_SHARK })
		.exec()
		.then((data) => {
            return data;
		})
		.catch((err) => {
			console.log(err);
		});
};
// 1.3. Get all tiger and bull sharks

const getTigerAndBullSharks = async () => {
	return Shark.find({ species: { $in: [TIGER_SHARK, BULL_SHARK] } })
		.exec()
		.then((data) => {
            return data;
		})
		.catch((err) => {
			console.log(err);
		});
};
// 1.4. Get all sharks except great white sharks
const getAllExceptGwShark = async () => {
	return Shark.find({ species: { $not: { $in: [GREAT_WHITE_SHARK] } } })
		.exec()
		.then((data) => {
            return data;
		})
		.catch((err) => {
			console.log(err);
		});
};
// 1.5. Get all sharks that have been known to attack
const getAttackSharks = async () => {
	return Attack.find({})
		.distinct("sharkId")
		.exec()
		.then((data) => {
			return Shark.find({ _id: { $in: data } })
				.exec()
				.then((data) => {
					return data
				})
				.catch((err) => {
					console.log(err);
				});
		})
		.catch((err) => {
			console.log(err);
		});
};

// 1.6. Get all areas with registered attacks
const getAllAreasWithRAttacks = async () => {
	return Attack.find({})
		.distinct("areaId")
		.exec()
		.then((data) => {
			return Area.find({ _id: { $in: data } })
				.exec()
				.then((data) => {
					return data;
				})
				.catch((err) => {
					console.log(err);
				});
		})
		.catch((err) => {
			console.log(err);
		});
};
// 1.7. Get all areas with more than 5 registered attacks
const getAreaWithMoreThanFiveAttacks = async () => {
	return Attack.aggregate([
		{
			$group: {
				_id: { areaId: "$areaId" },
				count: { $sum: 1 },
			},
		},
		{
			$match: {
				count: { $gte: 5 },
			},
		},
	])
		.exec()
		.then((data) => {
			let idLocations = [];
			for (let i in data) {
				idLocations.push(data[i]._id.areaId);
			}
			return Area.find({ _id: { $in: idLocations } })
				.exec()
				.then((data) => {
					return data;
				});
		});
};

// 1.8. Get the area with the most registered shark attacks
const getAreaWMostAttacks = async () => {
	return Attack.aggregate([
		{
			$group: {
				_id: { areaId: "$areaId" },
				count: { $sum: 1 },
			},
		},
	])
		.sort("-count")
		.exec()
		.then((data) => {
			var area = data[0]._id.areaId;
			return Area.find({ _id: area })
				.exec()
				.then((data) => {
					return data;
				});
		});
};
// 1.9. Get the total count of great white shark attacks
const getTotalCountOfGreatWhite = async () => {
	return Shark.findOne({ species: GREAT_WHITE_SHARK })
		.distinct("_id")
		.exec()
		.then((data) => {
			return Attack.aggregate([
				{
					$group: {
						_id: "$sharkId",
						count: { $sum: 1 },
					},
				},
			])
				.exec()
				.then((data) => {
					return data[0].count;
				});
		});
};

// 1.10. Get the total count of hammerhead and tiger shark attacks
const getTotalCountOfHammerAndTigerAttacks = async () => {
	return Shark.find({ species: { $in: [HAMMERHEAD_SHARK, TIGER_SHARK] } })
		.distinct("_id")
		.exec()
		.then((data) => {
			let h_t_id = [data[0]._id, data[1]._id];
			return Attack.aggregate([
				{
					$group: {
						_id: "$sharkId",
						count: { $sum: 1 },
					},
				},
				{
					$match: {
						_id: { $in: h_t_id },
					},
				},
			])
				.exec()
				.then((data) => {
					let tot_count = 0;
					for (let i in data) {
						tot_count += data[i].count;
					}
					return tot_count;
				});
		});
};


async function main() {

    console.log(1, "Get all sharks\n", await getAllSharks());
    console.log();
    console.log(2, "Get all Tiger sharks\n", await getTigerSharks());
    console.log();
    console.log(3, "Get all Tiger and Bull sharks\n", await getTigerAndBullSharks());
    console.log();
    console.log(4, "Get all except Great White shark\n", await getAllExceptGwShark());
    console.log();
    console.log(5, "Get all sharks that have been known to attack\n", await getAttackSharks());
    console.log();
    console.log(6, "Get all areas with registered attacks\n", await getAllAreasWithRAttacks());
    console.log();
    console.log(7, "Get all areas with more than 5 registered attacks\n", await getAreaWithMoreThanFiveAttacks());
    console.log();
    console.log(8, "Get the area with the most registered shark attacks\n", await getAreaWMostAttacks());
    console.log();
    console.log(9, "Get the total count of great white shark attacks\n", await getTotalCountOfGreatWhite());
    console.log();
    console.log(10, "Get the total count of hammerhead and tiger shark attacks\n", await getTotalCountOfHammerAndTigerAttacks());
}

main()